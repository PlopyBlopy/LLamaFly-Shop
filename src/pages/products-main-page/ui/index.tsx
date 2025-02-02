import { Result } from "antd";
import {
  productStore,
  sortOrderFilter,
  sortPropFilter,
} from "../../../entities/product";
import { observer } from "mobx-react-lite";
import { ProductCard } from "../../../features/product-card/index";
import { LoadingCircle } from "../../../features/loading-circle";
import styles from "./index.module.css";
import { DropDownFilter } from "../../../features/dropdown-filter";
import { QueryParams } from "../../../shared/product-service-products";
import { ImageUploader } from "../../../features/image-uploader/index";

export const ProductsMainPage = observer(() => {
  const {
    store: {
      productList,
      productListError,
      images,
      filterParams,
      isLoading,
      getProductCardListAction,
      setFilterParams,
    },
  } = productStore;

  if (productListError) {
    return <Result title={productListError} />;
  }

  // if (productListError) {
  //   return (
  //     <>
  //       <ImageUploader />
  //     </>
  //   );
  // }

  const handleFilterChange = (key: keyof QueryParams, value: string) => {
    setFilterParams({ ...filterParams, [key]: value });
    getProductCardListAction();
  };

  return (
    <div className={styles.container}>
      <div className={styles.filters}>
        <DropDownFilter
          label={"Сортировать по"}
          filters={sortPropFilter}
          currentValue={filterParams.sortProp}
          onValueChange={(value) => {
            handleFilterChange("sortProp", value);
          }}
        />
        <DropDownFilter
          label={"Порядок сортировки"}
          filters={sortOrderFilter}
          currentValue={filterParams.sortOrder}
          onValueChange={(value) => {
            handleFilterChange("sortOrder", value);
          }}
        />
      </div>

      {isLoading ? (
        <LoadingCircle />
      ) : (
        <div className={styles.grid}>
          {productList?.map(({ id, title, price, rating }) => (
            <ProductCard
              key={id}
              id={id}
              image={images[id]}
              title={title}
              price={price}
              rating={rating}
            />
          ))}
        </div>
      )}
    </div>
  );
});
