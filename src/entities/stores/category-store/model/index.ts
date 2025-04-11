import { action, makeObservable, observable, runInAction } from "mobx";
import {
  Category,
  getCategories,
} from "../../../../shared/services/product-service-categories";
import { AxiosInstance } from "axios";
// import { injectable } from "tsyringe";

// @injectable()
export default class CategoryStore {
  private readonly api: AxiosInstance;
  categoryList: Category[] = [];
  categoryListError = "";
  isLoading = false;

  constructor(api: AxiosInstance) {
    makeObservable(this, {
      categoryList: observable,
      categoryListError: observable,
      isLoading: observable,

      getCategoryListAction: action,
    });

    this.api = api;
  }

  getCategoryListAction = async () => {
    try {
      // Проверяем наличие данных и статус загрузки
      if (this.isLoading || this.categoryList.length > 0) return;

      // Устанавливаем статус загрузки внутри runInAction
      runInAction(() => {
        this.isLoading = true;
      });

      const data = await getCategories(this.api);

      // Обновляем состояние через runInAction
      runInAction(() => {
        this.categoryList = data!;
        this.isLoading = false;
        this.categoryListError = "";
      });
    } catch (error) {
      runInAction(() => {
        this.isLoading = false;
        this.categoryListError = (error as Error).message;
      });
    }
  };
}
