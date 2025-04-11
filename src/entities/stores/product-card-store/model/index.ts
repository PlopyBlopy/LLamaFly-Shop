import { action, makeObservable, observable, runInAction } from "mobx";
import FilterStore from "@/entities/stores/filter-store/model";
import ProductImageStore from "../../product-image-store/model";
import { getProductsCards, getProductsSellerCards, ProductCard } from "@/shared/services/product-service-products";
import { AxiosError, AxiosInstance } from "axios";
// import { injectable } from "tsyringe";

// @injectable()
export default class ProductCardStore {
  private readonly api: AxiosInstance;
  private readonly filterStore: FilterStore;
  private readonly productImageStore!: ProductImageStore;

  public productCardList: ProductCard[] = [];
  // public productCardListError: string = "";
  public productCardListError: AxiosError | null = null;
  public isLoading: boolean = false;

  constructor(api: AxiosInstance, filterStore: FilterStore, productImageStore: ProductImageStore) {
    makeObservable(this, {
      productCardList: observable,
      productCardListError: observable,
      isLoading: observable,
      getProductCardListAction: action,
      getProductsSellerCardsAction: action,
    });

    this.api = api;
    this.filterStore = filterStore;
    this.productImageStore = productImageStore;
  }

  getProductCardListAction = async () => {
    try {
      runInAction(() => {
        this.productCardList.length = 0;
        this.isLoading = true;
        this.productCardListError = null;
      });

      // 1. Получаем список товаров
      const products = await getProductsCards(this.api, this.filterStore.FilterParams);

      // 2. Запрашиваем изображения для полученных товаров
      const productIds = products.map((p) => p.id);
      const images = await this.productImageStore.getProductsCardsPreviewImagesAction(productIds);

      // 3. Объединяем данные
      runInAction(() => {
        this.productCardList = products.map((product) => ({
          ...product,
          image: images.get(product.id) || "",
        }));
      });
    } catch (error: AxiosError | any) {
      runInAction(() => {
        this.productCardListError = error;
      });
    } finally {
      runInAction(() => {
        this.isLoading = false;
      });
    }
  };

  getProductsSellerCardsAction = async (sellerId: string) => {
    try {
      runInAction(() => {
        this.productCardList.length = 0;
        this.isLoading = true;
        this.productCardListError = null;
      });

      // 1. Получаем список товаров
      const products = await getProductsSellerCards(this.api, sellerId);

      // 2. Запрашиваем изображения для полученных товаров
      const productIds = products.map((p) => p.id);
      const images = await this.productImageStore.getProductsCardsPreviewImagesAction(productIds);

      // 3. Объединяем данные
      runInAction(() => {
        this.productCardList = products.map((product) => ({
          ...product,
          image: images.get(product.id) || "",
        }));
      });
    } catch (error: AxiosError | any) {
      runInAction(() => {
        this.productCardListError = error;
      });
    } finally {
      runInAction(() => {
        this.isLoading = false;
      });
    }
  };

  // getProductCardListAction = async () => {
  //   try {
  //     this.isLoading = true;
  //     this.productCardListError = "";

  //     const params: QueryParams = this.filterStore.FilterParams;
  //     const products = await getProductCards(this.api, params);

  //     runInAction(() => {
  //       this.productCardList = products.map((product) => ({
  //         ...product,
  //         image: "",
  //       }));
  //     });

  //     const productIds: ProductIds = {
  //       productIds: this.productCardList.map((product) => product.id),
  //     };

  //     const images = await this.productImageStore.getProductsCardsImagesAction(
  //       productIds
  //     );

  //     runInAction(() => {
  //       this.productCardList.forEach((product) => {
  //         product.image = images.get(product.id) || "";
  //       });
  //     });
  //   } catch (error) {
  //     if (error instanceof Error) {
  //       runInAction(() => {
  //         this.productCardListError = error.message;
  //       });
  //     }
  //   } finally {
  //     runInAction(() => {
  //       this.isLoading = false;
  //     });
  //   }
  // };
}
