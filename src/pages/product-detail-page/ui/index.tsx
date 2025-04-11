import { Result } from "antd";
import { observer } from "mobx-react-lite";
import { useEffect, useRef } from "react";
import { useSearchParams } from "react-router-dom";
import { ProductDetail } from "../../../features/product-detail";
import { LoadingCircle } from "../../../features/loading-circle";
import { useStore } from "../../../shared/hooks/store-hook";

export const ProductDetailPage = observer(() => {
  const {
    productStore: { product, productError, isLoading, getProductAction },
  } = useStore();

  const [searchParams] = useSearchParams(); // Получаем searchParams
  const id = searchParams.get("id");

  const hasChecked = useRef(false);

  useEffect(() => {
    const checkProduct = async () => {
      if (id && hasChecked.current) return;
      hasChecked.current = true;

      await getProductAction(id!);
    };

    checkProduct();
  }, [id]);

  if (!product) {
    return <div>Product not found</div>; // Обработка случая, когда product отсутствует
  }

  return (
    <div>
      {isLoading ? (
        <LoadingCircle />
      ) : (
        <>
          {productError ? (
            <Result title={productError} />
          ) : (
            <ProductDetail product={product} />
          )}
        </>
      )}
    </div>
  );
});
