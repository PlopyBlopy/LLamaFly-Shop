import { DependencyContainer } from "tsyringe";
import { configureApiInterceptors } from "../../../../../shared/api-factory";
import { TYPES } from "../../config";

export class ApiInterceptorExtension {
  static addApiInterceptorServices(
    container: DependencyContainer
  ): DependencyContainer {
    configureApiInterceptors(
      container.resolve(TYPES.AuthServiceApi),
      container.resolve(TYPES.AuthStore)
    );

    configureApiInterceptors(
      container.resolve(TYPES.ProductServiceApi),
      container.resolve(TYPES.AuthStore)
    );

    configureApiInterceptors(
      container.resolve(TYPES.ImageServiceApi),
      container.resolve(TYPES.AuthStore)
    );

    configureApiInterceptors(
      container.resolve(TYPES.ProfileServiceApi),
      container.resolve(TYPES.AuthStore)
    );

    return container;
  }
}
