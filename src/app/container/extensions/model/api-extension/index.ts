import { DependencyContainer } from "tsyringe";
import {
  AuthServiceApiConfig,
  ImageServiceApiConfig,
  ProductServiceApiConfig,
  ProfileServiceApiConfig,
} from "../../../../../shared/api/http-client";
import { TYPES } from "../../config";
import { createApi } from "../../../../../shared/api-factory";

export class ApiExtension {
  static addApiServices(container: DependencyContainer): DependencyContainer {
    const authServiceApi = createApi(AuthServiceApiConfig);
    const productServiceApi = createApi(ProductServiceApiConfig);
    const imageServiceApi = createApi(ImageServiceApiConfig);
    const profileServiceApi = createApi(ProfileServiceApiConfig);

    container.registerInstance(TYPES.AuthServiceApi, authServiceApi);
    container.registerInstance(TYPES.ProductServiceApi, productServiceApi);
    container.registerInstance(TYPES.ImageServiceApi, imageServiceApi);
    container.registerInstance(TYPES.ProfileServiceApi, profileServiceApi);

    return container;
  }
}
