import { makeAutoObservable, runInAction } from "mobx";
import { Category, getCategories } from "../../../../shared/product-service-categories";

export default class CategoryStore{
    categoryList: Category[] = [];
    categoryListError = '';
    isLoading = false;

    constructor(){
        makeAutoObservable(this);
    }

    getCategoryListAction = async() => {
        try {
            this.isLoading = true;

            const data = await getCategories();

            runInAction(() => {
                this.categoryList = data;

            });
        } catch (error) {
            if (error instanceof Error) {
                runInAction(() => {
                    this.categoryListError = error.message;
                });
            }
        }
        this.isLoading = false;
    }
}