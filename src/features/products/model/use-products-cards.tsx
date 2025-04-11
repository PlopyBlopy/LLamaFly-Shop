// import { useStore } from "@/shared/store";

// export const useProducts = () => {
//   const { productCardStore } = useStore();

//   const loadProducts = async () => {
//     try {
//       await productCardStore.getProductCardListAction();
//     } catch (e) {
//       // Обработка ошибок
//     }
//   };

//   return {
//     products: productCardStore.productCardList,
//     error: productCardStore.productCardListError,
//     isLoading: productCardStore.isLoading,
//     loadProducts,
//   };
// };
