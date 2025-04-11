import { useEffect, useCallback } from "react";
import { Result, Button } from "antd";
import { useStore } from "@/shared/hooks/store-hook";
import { ProductsSellerComp } from "@/widgets/products-seller-comp";
import { useAppNavigate } from "@/shared/routing/routes";
import { LoadingCircle } from "@/features/loading-circle";
import { observer } from "mobx-react-lite";

export const ProductsSellerPage = observer(() => {
  const { goToProduct } = useAppNavigate();
  const {
    profileStore: { profile, isLoading: isProfileLoading },
    productCardStore: { productCardList, isLoading: isProductCardLoading, productCardListError, getProductsSellerCardsAction },
  } = useStore();
  const profileId = profile?.id;

  // Загрузка данных профиля
  if (isProfileLoading) {
    return <LoadingCircle />;
  }

  // Оптимизация: использование useCallback для мемоизации функции
  const handleProductDetailPage = useCallback(
    (productTitle: string, productId: string) => {
      goToProduct(productTitle, productId);
    },
    [goToProduct]
  );

  useEffect(() => {
    const fetchProducts = async () => {
      if (profileId !== undefined) {
        await getProductsSellerCardsAction(profileId);
      }
    };

    fetchProducts();
  }, [profileId, getProductsSellerCardsAction]); // Добавлены зависимости

  const handleRetry = useCallback(async () => {
    if (profileId !== undefined) {
      await getProductsSellerCardsAction(profileId);
    }
  }, [profileId, getProductsSellerCardsAction]);

  // Упрощенная структура рендеринга
  if (isProductCardLoading) {
    return <LoadingCircle />;
  }

  if (productCardListError) {
    return (
      <>
        {productCardListError.status === 404 ? (
          <Result
            status="info"
            title="У вас нет товаров."
            subTitle={productCardListError.message}
            extra={[
              <Button type="primary" key="retry" onClick={handleRetry}>
                Повторить попытку
              </Button>,
            ]}
          />
        ) : (
          <Result
            status="error"
            title="Ошибка"
            subTitle={productCardListError.message}
            extra={[
              <Button type="primary" key="retry" onClick={handleRetry}>
                Повторить попытку
              </Button>,
            ]}
          />
        )}
      </>
    );
  }

  return <ProductsSellerComp productCardList={productCardList} onProductDetailPage={handleProductDetailPage} />;
});

// import { useStore } from "../../../shared/hooks/store-hook";
// import { ProductsSellerComp } from "../../../widgets/products-seller-comp";
// import { useAppNavigate } from "../../../shared/routing/routes";
// import { LoadingCircle } from "../../../features/loading-circle";
// import { useEffect } from "react";
// // import { ProductCard } from "../../../shared/services/product-service-products";
// import { Result } from "antd";

// export const ProductsSellerPage = () => {
//   const { goToProduct } = useAppNavigate();
//   const {
//     profileStore: { profile },
//     productCardStore: { productCardList, isLoading, productCardListError, getProductsSellerCardsAction },
//   } = useStore();

//   const profileId = profile?.id;

//   // const [products, setProducts] = useState<ProductCard[]>();

//   useEffect(() => {
//     const getProductsSeller = async () => {
//       if (profileId != null) await getProductsSellerCardsAction(profileId);
//     };

//     if (productCardList.length === 0)
//       getProductsSeller();
//   }, [profileId]);

//   const handleProductDetailPage = (productTitle: string, productId: string) => {
//     goToProduct(productTitle, productId);
//   };

//   //    const hasChecked = useRef(false);

//   //     useEffect(() => {
//   //       const checkProduct = async () => {
//   //         if (id && hasChecked.current) return;
//   //         hasChecked.current = true;

//   //         await getProductAction(id!);
//   //       };

//   //       checkProduct();
//   //     }, [id]);

//   return (
//     <>
//       {isLoading ? (
//         <LoadingCircle />
//       ) : (
//         <>
//           {productCardListError ? (
//             <Result title={productCardListError} />
//           ) : (
//             <ProductsSellerComp productCardList={productCardList} onProductDetailPage={handleProductDetailPage} />
//           )}
//         </>
//       )}
//     </>
//   );
// };
