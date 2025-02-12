import { makeAutoObservable, runInAction } from "mobx";
import ProductImageStore from "../../product-image-store/model";
import {
  addProduct,
  getProductById,
  Product,
  ProductCreate,
} from "../../../../shared/product-service-products";
import { ProductForm } from "../../../../features/product-add-form";

export default class ProductStore {
  private productImageStore!: ProductImageStore;

  public product: Product | null = null;
  public productError: string = "";
  public isLoading: boolean = false;

  constructor() {
    makeAutoObservable(this);
  }

  setDependencies(deps: { productImageStore: ProductImageStore }) {
    this.productImageStore = deps.productImageStore;
  }

  addProductAction = async (product: ProductForm) => {
    try {
      this.isLoading = true;
      this.productError = "";

      const productCreate: ProductCreate = {
        title: product.title,
        description: product.description,
        price: product.price,
        categoryId: product.categoryId,
        sellerId: product.sellerId,
      };

      await addProduct(productCreate);
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

  getProductAction = async (id: string) => {
    try {
      this.isLoading = true;
      this.productError = "";

      const data = await getProductById(id);

      runInAction(() => {
        this.product = data;
      });

      // Загружаем изображение для продукта
      if (this.product != null)
        await this.productImageStore.getProductImageAction(this.product);
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
