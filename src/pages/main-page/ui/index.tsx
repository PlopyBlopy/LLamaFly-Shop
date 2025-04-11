import { Result } from "antd";
import { sortOrderFilter, sortPropFilter } from "../../../entities/stores/filter-store";
import { observer } from "mobx-react-lite";
import { ProductItemCard } from "../../../features/product-card/index";
import { LoadingCircle } from "../../../features/loading-circle";
import styles from "./index.module.css";
import { DropDownFilter } from "../../../features/dropdown-filter";
import { useAppNavigate } from "../../../shared/routing/routes";
import { QueryParams } from "../../../shared/services/product-service-products";
import { useStore } from "../../../shared/hooks/store-hook";
import { useEffect } from "react";

export const MainPage = observer(() => {
  const { goToProduct } = useAppNavigate();
  const {
    productCardStore: { productCardList, productCardListError, isLoading, getProductCardListAction },
    filterStore: { FilterParams, setFilterParams },
  } = useStore();

  if (productCardListError) {
    return <Result title={productCardListError.message} />;
  }

  // useEffect(() => {
  //   if (productCardList.length == 0) {
  //     getProductCardListAction();
  //   }
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

  const handleFilterChange = async (key: keyof QueryParams, value: string) => {
    setFilterParams({ ...FilterParams, [key]: value });
    await getProductCardListAction();
  };

  const onProductDetailPageOpen = (cardTitle: string, cardId: string) => {
    goToProduct(cardTitle, cardId);
  };

  return (
    <div className={styles.container}>
      <div className={styles.filters}>
        <DropDownFilter
          label={"Сортировать по"}
          filters={sortPropFilter}
          currentValue={FilterParams.sortProp}
          onValueChange={(value) => {
            handleFilterChange("sortProp", value);
          }}
        />
        <DropDownFilter
          label={"Порядок сортировки"}
          filters={sortOrderFilter}
          currentValue={FilterParams.sortOrder}
          onValueChange={(value) => {
            handleFilterChange("sortOrder", value);
          }}
        />
      </div>

      {isLoading ? (
        <LoadingCircle />
      ) : (
        <div className={styles.grid}>
          {productCardList?.map((product) => (
            <ProductItemCard key={product.id} card={product} onCardClick={onProductDetailPageOpen} />
          ))}
        </div>
      )}
    </div>
  );
});
