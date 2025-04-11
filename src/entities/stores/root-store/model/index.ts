import { makeAutoObservable } from "mobx";
import AuthStore from "../../auth-store/model";
import ProfileStore from "../../profile-store/model";
import {
  configureApiInterceptors,
  createApi,
} from "../../../../shared/api-factory";
import { AxiosInstance } from "axios";
import {
  AuthServiceApiConfig,
  ImageServiceApiConfig,
  ProductServiceApiConfig,
  ProfileServiceApiConfig,
} from "../../../../shared/api/http-client";
import FilterStore from "../../filter-store/model";
import CategoryStore from "../../category-store/model";
import ProductImageStore from "../../product-image-store/model";
import ProductCardStore from "../../product-card-store/model";
import ProductStore from "../../product-store/model";

export class RootStore {
  authServiceApi: AxiosInstance;
  productServiceApi: AxiosInstance;
  imageServiceApi: AxiosInstance;
  profileServiceApi: AxiosInstance;

  authStore: AuthStore;
  profileStore: ProfileStore;
  productStore: ProductStore;
  productCardStore: ProductCardStore;
  categoryStore: CategoryStore;
  filterStore: FilterStore;
  productImageStore: ProductImageStore;

  constructor() {
    this.authServiceApi = createApi(AuthServiceApiConfig);
    this.productServiceApi = createApi(ProductServiceApiConfig);
    this.imageServiceApi = createApi(ImageServiceApiConfig);
    this.profileServiceApi = createApi(ProfileServiceApiConfig);

    this.authStore = new AuthStore(this.authServiceApi);
    this.profileStore = new ProfileStore(
      this.profileServiceApi,
      this.authStore
    );
    this.productImageStore = new ProductImageStore(this.imageServiceApi);
    this.filterStore = new FilterStore();
    this.productStore = new ProductStore(
      this.productServiceApi,
      this.productImageStore
    );
    this.productCardStore = new ProductCardStore(
      this.productServiceApi,
      this.filterStore,
      this.productImageStore
    );
    this.categoryStore = new CategoryStore(this.productServiceApi);

    configureApiInterceptors(this.authServiceApi, this.authStore);
    configureApiInterceptors(this.productServiceApi, this.authStore);
    configureApiInterceptors(this.imageServiceApi, this.authStore);
    configureApiInterceptors(this.profileServiceApi, this.authStore);

    makeAutoObservable(this);
  }
}
