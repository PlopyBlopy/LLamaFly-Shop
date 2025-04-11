import { action, makeObservable, observable, runInAction } from "mobx";
import {
  Admin,
  Customer,
  getAdminProfile,
  getCustomerProfile,
  getSellerProfile,
  Seller,
  UserRoleType,
} from "../../../../shared/services/profile-service";
import { AxiosInstance } from "axios";
import AuthStore from "../../auth-store/model";

export default class ProfileStore {
  profile: Admin | Seller | Customer | null = null;
  isUnAuthorize: boolean = true;
  isLoading: boolean = false;
  profileError: string = "";
  private readonly api: AxiosInstance;
  private readonly authStore: AuthStore;

  constructor(api: AxiosInstance, authStore: AuthStore) {
    makeObservable(this, {
      profile: observable,
      profileError: observable,
      installUserRoleAction: action,
      getAdminProfileAction: action,
      getSellerProfileAction: action,
      getCustomerProfileAction: action,
      logoutAction: action,
    });

    this.api = api;
    this.authStore = authStore;
    this.installUserRoleAction();
  }

  installUserRoleAction = async () => {
    runInAction(() => {
      this.isLoading = true;
    });

    const user = await this.authStore.getUserPayloadAction();

    if (!user) {
      runInAction(() => {
        this.isUnAuthorize = true;
        this.profileError = "Пользователь не авторизован";
        this.isLoading = false;
      });

      return;
    }

    if (user) {
      let profile;
      switch (user.role) {
        case UserRoleType.Admin:
          profile = await this.getAdminProfileAction();
          break;
        case UserRoleType.Seller:
          profile = await this.getSellerProfileAction();
          break;
        case UserRoleType.Customer:
          profile = await this.getCustomerProfileAction();
          break;
        default:
          throw new Error("Unknown user role");
      }

      runInAction(() => {
        if (profile) {
          this.profile = observable({
            // Делаем объект observable
            ...profile,
            id: user.sub,
            role: user.role,
          });
          this.isUnAuthorize = false;
          this.isLoading = false;
        }
      });
    }
  };

  getAdminProfileAction = async (): Promise<Admin> => {
    const response = await getAdminProfile(this.api);
    return response as Admin;
  };

  getSellerProfileAction = async (): Promise<Seller> => {
    const response = await getSellerProfile(this.api);
    return response as Seller;
  };

  getCustomerProfileAction = async (): Promise<Customer> => {
    const response = await getCustomerProfile(this.api);
    return response as Customer;
  };

  logoutAction = async () => {
    runInAction(() => {
      this.profile = null;
      this.profileError = "";
      this.isUnAuthorize = true;
    });

    await this.authStore.logoutAction();
  };
}
