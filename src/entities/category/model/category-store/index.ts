import { makeAutoObservable, runInAction } from "mobx";
import { Category } from "../../../../shared/product-service-categories";
import { getCategories } from "../../../../shared/product-service-categories";

class CategoryStoree {
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

export const store = new CategoryStoree(); 