import { Result } from "antd";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import { ProductDetail } from "../../../features/product-detail";
import { LoadingCircle } from "../../../features/loading-circle";
import { useStore } from "../../../shared/contexts/store-context";

export const ProductDetailPage = observer(() => {
  const rootStore = useStore();

  const {
    productStore: { getProductAction, product, productError, isLoading },
  } = rootStore;

  const [searchParams] = useSearchParams(); // Получаем searchParams
  const id = searchParams.get("id");

  useEffect(() => {
    if (id) {
      getProductAction(id);
    }
  }, [id, getProductAction]);

  if (productError) {
    return <Result title={productError} />;
  }
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
// image={images[product.id]}
