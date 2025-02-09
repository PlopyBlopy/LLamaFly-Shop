// import { makeAutoObservable, runInAction } from "mobx";
// import { getProductById, getProductCards, getRandomImageFromUnsplash, Product, ProductCard } from "../../../../shared/product-service-products/index"
// import { FilterParams } from "../filter";
// import { CATEGORYID, SEARCH, SORTORDER, SORTPROP } from "../../config/filters/default-values";

// class ProductCardStore{
//   productCardList: ProductCard[] = [];
//   isLoading = false;
//   productListError = '';
//   filterParams: FilterParams = {
//     search: SEARCH,
//     categoryId: CATEGORYID, 
//     sortProp: SORTPROP,
//     sortOrder: SORTORDER
//   };

//   constructor(){
//       makeAutoObservable(this);
//       this.getProductCardListAction();
//   }

//   setFilterParams = (newParams: Partial<FilterParams>) => {
//     this.filterParams = { ...this.filterParams, ...newParams };
//   }
 
// // Загрузка списка продуктов
//   getProductCardListAction = async () => {
//     try {
//         this.isLoading = true;
        
//         const data = await getProductCards(this.filterParams);
//         runInAction(() => {
//           this.productCardList = data;
//         });
//         // После загрузки продуктов загружаем изображения
//         this.getProductImagesAction();
//       } 
//       catch (error) {
//         if (error instanceof Error) {
//           runInAction(() => {
//             this.isLoading = false;
//             this.productListError = error.message;
//           });
//         }
//       }
//       finally {
//         runInAction(() => {
//           this.isLoading = false;
//         });
//       }
//     };

//   getProductImagesAction = async () => {
//     try {
//       this.isLoading = true;


//       // Параллельная загрузка изображений для всех продуктов
//       const imagePromises = this.productCardList.map(async (product) => {
//         const imageUrl = await getRandomImageFromUnsplash();
//         return { productId: product.id, imageUrl };
//       });

//       const results = await Promise.all(imagePromises);

//       runInAction(() => {
//         results.forEach(({ productId, imageUrl }) => {
//           const targetProduct = this.productCardList.find(p => p.id === productId);
//           if (targetProduct) {
//             targetProduct.image = imageUrl; // Прямое обновление поля Image
//           }
//         });
//       });
//     }
//     catch (error) {
//         if (error instanceof Error) {
//           runInAction(() => {
//             this.productListError = error.message;
//           });
//         }
//       } finally {
//         runInAction(() => {
//           this.isLoading = false;
//         });
//       }
//   };
// }

// export const store = new ProductCardStore();