import { Result } from "antd";
import { observer } from "mobx-react-lite";
import { productStore } from "../../../entities/product";
import { useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import { ProductDetail } from "../../../features/product-detail";
import { LoadingCircle } from "../../../features/loading-circle";

export const ProductDetailPage = observer(() => {
  const {
    store: { getProductByIdAction, isLoading, product, productError, images },
  } = productStore;

  const [searchParams] = useSearchParams(); // Получаем searchParams
  const id = searchParams.get("id");

  useEffect(() => {
    if (id) {
      getProductByIdAction(id);
    }
  }, [id, getProductByIdAction]);

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
            <ProductDetail product={product} image={images[product.id]} />
          )}
        </>
      )}
    </div>
  );
});
