import { useEffect } from "react";
import {
  ProductAddForm,
  ProductForm,
} from "../../../features/product-add-form";
import { useStore } from "../../../shared/contexts/store-context";

export const AddProductPage = () => {
  const rootStore = useStore();
  const {
    productStore: { productError, addProductAction },
    categoryStore: { categoryList, isLoading, getCategoryListAction },
    // profileStore: { profile },
    productImageStore: { addProductImagesAction },
  } = rootStore;

  const handleAddProduct = (product: ProductForm) => {
    addProductAction(product);

    if (!productError) {
      addProductImagesAction(product.images);
    }
  };

  useEffect(() => {
    // Проверяем что категории еще не загружены и нет процесса загрузки
    if (categoryList.length === 0 && !isLoading) {
      getCategoryListAction();
    }
  }, [categoryList.length, isLoading]);

  return (
    <>
      <ProductAddForm
        onSubmit={handleAddProduct}
        categories={categoryList}
        sellers={[
          "550e8400-e29b-41d4-a716-446655440000",
          "550e8400-e29b-41d4-a716-446655440000",
        ]}
      />
    </>
  );
};
