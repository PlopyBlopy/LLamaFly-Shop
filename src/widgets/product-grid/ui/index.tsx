import { Result } from "antd";
import { ProductItemCard } from "features/product-card";
import { LoadingCircle } from "@/features/loading-circle";
import { useStore } from "@/shared/hooks/store-hook";
import { useEffect } from "react";
import styles from "./index.module.css";

type Props = {
  onGoToPage: (title: string, id: string) => void;
};

export const ProductGrid = ({ onGoToPage }: Props) => {
  const {
    productCardStore: { productCardList, productCardListError, isLoading, getProductCardListAction },
  } = useStore();

  // useEffect(() => {
  //   const getProductCartList = async () => {
  //     await getProductCardListAction();
  //   };

  //   if (productCardList.length == 0) getProductCartList();
  // }, [productCardList.length]);

  useEffect(() => {
    const fetchData = async () => {
      if (productCardList.length === 0 && !isLoading) {
        // ✅ Учитываем состояние загрузки
        await getProductCardListAction();
      }
    };
    fetchData();
  }, [productCardList.length, isLoading]); // Добавляем зависимость

  if (productCardListError) {
    return <Result title={productCardListError.message} />;
  }

  return (
    <>
      {isLoading ? (
        <LoadingCircle />
      ) : (
        <div className={styles.grid}>
          {productCardList?.map((product) => (
            <ProductItemCard key={product.id} card={product} onCardClick={onGoToPage} />
          ))}
        </div>
      )}
    </>
  );
};
