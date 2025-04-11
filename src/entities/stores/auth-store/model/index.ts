import { jwtDecode } from "jwt-decode";
import { action, makeObservable, observable, runInAction } from "mobx";
import { AxiosInstance } from "axios";
import { refresh } from "@/shared/services/auth-service-token";
import {
  CustomerRegister,
  login,
  logout,
  ProfileRegister,
  registerCustomer,
  registerSeller,
  SellerRegister,
  UserProfileCustomerRegister,
  UserLogin,
  UserRegister,
  UserProfileSellerRegister,
} from "@/shared/services/auth-service-user";
import { UserPayload } from "@/shared/services/profile-service";
// @singleton()
export default class AuthStore {
  accessToken: string | null = null;
  isAuthenticated: boolean = false;
  private readonly api: AxiosInstance;
  private isRefreshing: boolean = false;
  private pendingRequests: Array<(_token: string) => void> = [];

  constructor(api: AxiosInstance) {
    makeObservable(this, {
      accessToken: observable,
      isAuthenticated: observable,

      refreshTokenAction: action,
      getUserPayloadAction: action,
      loginAction: action,
      logoutAction: action,
      registerSellerAction: action,
      registerCustomerAction: action,
    });

    this.api = api;

    this.initializeFromStorage();
  }

  private initializeFromStorage() {
    runInAction(() => {
      const token = localStorage.getItem("accessToken");
      this.accessToken = token;
      this.isAuthenticated = !!token;
    });
  }

  refreshTokenAction = async () => {
    if (this.isRefreshing) {
      return new Promise<string>((resolve) => {
        this.pendingRequests.push((token) => resolve(token));
      });
    }

    this.isRefreshing = true;
    try {
      const response = await refresh(this.api);

      runInAction(() => {
        this.accessToken = response.accessToken;
        this.isAuthenticated = true;
        localStorage.setItem("accessToken", this.accessToken!);
      });
      return this.accessToken!;
    } catch (error) {
      runInAction(() => this.logoutAction());
      throw error;
    } finally {
      this.isRefreshing = false;
      const token = this.accessToken!;
      this.pendingRequests.forEach((resolve) => resolve(token));
      this.pendingRequests = [];
    }
  };

  getUserPayloadAction = async (): Promise<UserPayload | null> => {
    if (this.isAuthenticated && this.accessToken) {
      return await jwtDecode<UserPayload>(this.accessToken);
    }
    return null;
  };

  loginAction = async (user: UserLogin) => {
    try {
      const response = await login(this.api, user);
      runInAction(() => {
        this.accessToken = response.accessToken;
        this.isAuthenticated = true;
        localStorage.setItem("accessToken", this.accessToken!);
      });
    } catch (error) {
      runInAction(() => this.logoutAction());
      throw error;
    }
  };

  logoutAction = async () => {
    runInAction(async () => {
      this.accessToken = null;
      this.isAuthenticated = false;
    });

    localStorage.removeItem("accessToken");

    await logout(this.api);
  };

  registerSellerAction = async (user: UserRegister, profile: ProfileRegister, seller: SellerRegister) => {
    const payload: UserProfileSellerRegister = { user, profile, seller };
    await registerSeller(this.api, payload);
  };

  registerCustomerAction = async (user: UserRegister, profile: ProfileRegister, customer: CustomerRegister) => {
    const payload: UserProfileCustomerRegister = { user, profile, customer };
    await registerCustomer(this.api, payload);
  };
}
