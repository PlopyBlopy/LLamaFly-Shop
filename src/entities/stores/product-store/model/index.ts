import { action, makeObservable, observable, runInAction } from "mobx";
import ProductImageStore from "../../product-image-store/model";

import { ProductForm } from "../../../../features/product-add-form";
import { addProduct, getProductById, Product, ProductCreate } from "../../../../shared/services/product-service-products";
import { AxiosInstance } from "axios";

export default class ProductStore {
  private readonly api: AxiosInstance;
  private productImageStore: ProductImageStore;

  product: Product | null = null;
  productError: string = "";
  isLoading: boolean = false;

  constructor(api: AxiosInstance, productImageStore: ProductImageStore) {
    makeObservable(this, {
      product: observable,
      productError: observable,
      isLoading: observable,
      addProductAction: action,
      getProductAction: action,
    });

    this.api = api;
    this.productImageStore = productImageStore;
  }

  addProductAction = async (product: ProductForm): Promise<string | null> => {
    try {
      runInAction(() => {
        this.isLoading = true;
        this.productError = "";
      });

      const productCreate: ProductCreate = {
        title: product.title,
        description: product.description,
        price: product.price,
        categoryId: product.categoryId,
        sellerId: product.sellerId,
      };

      const response = await addProduct(this.api, productCreate);
      return response;
    } catch (error) {
      if (error instanceof Error) {
        runInAction(() => {
          this.productError = error.message;
        });
      }
      return null;
    } finally {
      runInAction(() => {
        this.isLoading = false;
      });
    }
  };

  getProductAction = async (id: string) => {
    try {
      this.isLoading = true;
      this.productError = "";

      const data = await getProductById(this.api, id);

      runInAction(() => {
        this.product = data;
      });

      // Загружаем изображение для продукта
      if (this.product != null) {
        // await this.productImageStore.getProductImageAction(this.product);
        const images = await this.productImageStore.getProductImagesAction(this.product.id);
        this.product.images = images;
      }
    } catch (error) {
      if (error instanceof Error) {
        runInAction(() => {
          this.productError = error.message;
        });
      }
    } finally {
      runInAction(() => {
        this.isLoading = false;
      });
    }
  };
}
