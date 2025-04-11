import { useEffect, useState } from "react";
import { ProductAddForm, ProductForm, SellerProp } from "../../../features/product-add-form";
import { observer } from "mobx-react-lite";
import { useStore } from "../../../shared/hooks/store-hook";
import { Result } from "antd";
import { ImagesUpload } from "@/shared/services/image-service-product-images";

export const AddProductPage = observer(() => {
  const {
    productStore: { productError, addProductAction },
    categoryStore: { categoryList, isLoading, getCategoryListAction },
    profileStore: { profile, profileError },
    productImageStore: { addProductImagesAction },
  } = useStore();

  const [initialLoadDone, setInitialLoadDone] = useState(false);

  if (!profile) {
    return <Result>{profileError}</Result>;
  }

  const handleAddProduct = async (product: ProductForm) => {
    const productId = await addProductAction(product);

    if (!productError && productId) {
      const imagesUpload: ImagesUpload = {
        productId: productId,
        images: product.images,
      };
      await addProductImagesAction(imagesUpload);
    }
  };

  useEffect(() => {
    const getCategories = async () => {
      await getCategoryListAction();
      setInitialLoadDone(true); // Отмечаем завершение загрузки
    };

    // Добавляем проверку на initialLoadDone
    if (!initialLoadDone && categoryList.length === 0 && !isLoading) {
      getCategories();
    }
  }, [categoryList, isLoading, initialLoadDone, getCategoryListAction]);

  const sellerData: SellerProp = {
    id: profile!.id,
    name: profile!.name,
    surname: profile!.surname,
    patronymic: profile!.patronymic,
  };

  return <ProductAddForm onSubmit={handleAddProduct} categories={categoryList} seller={sellerData} />;
});
