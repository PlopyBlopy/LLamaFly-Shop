import { makeAutoObservable, runInAction } from "mobx";
import {
  getProductCards,
  ProductCard,
  QueryParams,
} from "../../../../shared/product-service-products";
import FilterStore from "../../filter-store/model";
import ProductImageStore from "../../product-image-store/model";

export default class ProductCardStore {
  private filterStore!: FilterStore;
  private productImageStore!: ProductImageStore;

  public productCardList: ProductCard[] = [];
  public productCardListError: string = "";
  public isLoading: boolean = false;

  constructor() {
    makeAutoObservable(this);
  }

  setDependencies(deps: {
    filterStore: FilterStore;
    productImageStore: ProductImageStore;
  }) {
    this.filterStore = deps.filterStore;
    this.productImageStore = deps.productImageStore;

    // изменить
    this.getProductCardListAction();
  }
  getProductCardListAction = async () => {
    try {
      this.isLoading = true;
      this.productCardListError = "";

      const params: QueryParams = this.filterStore.FilterParams;
      const products = await getProductCards(params);

      runInAction(() => {
        this.productCardList = products.map((product) => ({
          ...product,
          image: "", // Инициализируем пустым значением
        }));
      });

      // Загружаем изображения для каждого продукта
      await this.productImageStore.getProductsCardsImagesAction(
        this.productCardList
      );
    } catch (error) {
      if (error instanceof Error) {
        runInAction(() => {
          this.productCardListError = error.message;
        });
      }
    } finally {
      runInAction(() => {
        this.isLoading = false;
      });
    }
  };
}
