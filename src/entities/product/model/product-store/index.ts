import { makeAutoObservable, runInAction } from "mobx";
import { getProductById, getProductCards, getRandomImageFromUnsplash, Product, ProductCard } from "../../../../shared/product-service-products/index"
import { FilterParams } from "../filter";
import { CATEGORYID, SEARCH, SORTORDER, SORTPROP } from "../../config/filters/default-values";

class ProductStore{
  productList: ProductCard[] = [];
  product?: Product;
  images: { [key: string]: string } = {}; 
  isLoading = false;
  isUpdateLoading = false;
  productListError = '';
  productError = '';
  filterParams: FilterParams = {
    search: SEARCH,
    categoryId: CATEGORYID, 
    sortProp: SORTPROP,
    sortOrder: SORTORDER
  };

  constructor(){
      makeAutoObservable(this);
      this.getProductCardListAction();
  }

  setFilterParams = (newParams: Partial<FilterParams>) => {
    this.filterParams = { ...this.filterParams, ...newParams };
  }


  // getProductCardListAction = async (params: QueryParams) => {
    //     try{
    //         this.isLoading = true;

    //         const data = await getProductCards(params); 

    //         runInAction(() => {
    //             this.isLoading = false;
    //             this.productList = data;
    //         });
    //     }
    //     catch(error){
    //         if(error instanceof Error){
    //             runInAction(() => {
    //                 this.isLoading =false;
    //                 this.productListError = error.message;
    //             });
    //         }
    //     }
    // }



      // Загрузка изображений для всех продуктов
 
// Загрузка списка продуктов
  getProductCardListAction = async () => {
    try {
        this.isLoading = true;
        
        const data = await getProductCards(this.filterParams);
        runInAction(() => {
          this.productList = data;
        });
        // После загрузки продуктов загружаем изображения
        this.getProductImagesAction();
      } catch (error) {
        if (error instanceof Error) {
          runInAction(() => {
            this.isLoading = false;
            this.productListError = error.message;
          });
        }
      }
      this.isLoading = false;
    };

  getProductByIdAction = async(id: string) => {
      try{
          this.isLoading = true;

          const data = await getProductById(id); 

          runInAction(() => {
              this.isLoading = false;
              this.product = data;
          });
          this.getProductImagesAction();
      }
      catch(error){
          if(error instanceof Error){
              runInAction(() => {
                  this.isLoading = false;
                  this.productListError = error.message;
              });
          }
      }
  }
  
  getProductImagesAction = async () => {
    try {
      this.isLoading = true;

      // Загружаем изображения для каждого продукта
      for (const product of this.productList) {
        const imageUrl = await getRandomImageFromUnsplash();
        runInAction(() => {
          this.images[product.id] = imageUrl; // Сохраняем изображение по ID продукта
        });
      }
    } catch (error) {
      if (error instanceof Error) {
        runInAction(() => {
          this.isLoading = false;
          this.productListError = error.message;
        });
      }
    } finally {
      runInAction(() => {
        this.isLoading = false;
      });
    }
  };
    // updateProductAction = async(product: Product) => {

    // }
}

export const store = new ProductStore();