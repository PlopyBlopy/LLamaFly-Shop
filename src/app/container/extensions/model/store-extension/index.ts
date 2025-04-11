import { DependencyContainer } from "tsyringe";
import { TYPES } from "../../config";
import { AxiosInstance } from "axios";
import AuthStore from "../../../../../entities/stores/auth-store/model";
import ProfileStore from "../../../../../entities/stores/profile-store/model";
import FilterStore from "../../../../../entities/stores/filter-store/model";
import ProductImageStore from "../../../../../entities/stores/product-image-store/model";
import CategoryStore from "../../../../../entities/stores/category-store/model";
import ProductCardStore from "../../../../../entities/stores/product-card-store/model";
import ProductStore from "../../../../../entities/stores/product-store/model";

export class StoreExtension {
  static addStoreServices(container: DependencyContainer): DependencyContainer {
    container.registerSingleton(TYPES.FilterStore, FilterStore);
    container.registerSingleton(TYPES.ProductImageStore, ProductImageStore);

    container.register(TYPES.AuthStore, {
      useFactory: (c) => {
        return new AuthStore(c.resolve<AxiosInstance>(TYPES.AuthServiceApi));
      },
    });

    container.register(TYPES.ProfileStore, {
      useFactory: (c) => {
        return new ProfileStore(
          c.resolve<AxiosInstance>(TYPES.ProfileServiceApi),
          c.resolve<AuthStore>(TYPES.AuthStore)
        );
      },
    });

    container.register(TYPES.ProductStore, {
      useFactory: (c) => {
        return new ProductStore(
          c.resolve<AxiosInstance>(TYPES.ProductServiceApi),
          c.resolve<ProductImageStore>(TYPES.ProductImageStore)
        );
      },
    });

    container.register(TYPES.ProductCardStore, {
      useFactory: (c) => {
        return new ProductCardStore(
          c.resolve<AxiosInstance>(TYPES.ProductServiceApi),
          c.resolve<FilterStore>(TYPES.FilterStore),
          c.resolve<ProductImageStore>(TYPES.ProductImageStore)
        );
      },
    });

    container.register(TYPES.CategoryStore, {
      useFactory: (c) => {
        return new CategoryStore(
          c.resolve<AxiosInstance>(TYPES.ProductServiceApi)
        );
      },
    });

    return container;
  }
}

// container.registerSingleton(TYPES.AuthStore, AuthStore);
// container.registerSingleton(TYPES.ProfileStore, ProfileStore);

// const productStore = container.resolve(TYPES.ProductStore);
// const imageStore = container.resolve(TYPES.ProductImageStore);

// productStore.imageStore = imageStore;
// imageStore.productStore = productStore;
