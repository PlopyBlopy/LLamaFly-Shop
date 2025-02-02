import { observer } from "mobx-react-lite";
import { CategoryDrawer } from "../../../features/category-drawer";
import { categoryStore } from "../../../entities/category";
import { Result } from "antd";
import { productStore } from "../../../entities/product";
import {
  SEARCH,
  SORTORDER,
  SORTPROP,
} from "../../../entities/product/config/filters/default-values";

export const CategoryMenu = observer(() => {
  const {
    store: {
      categoryList,
      categoryListError,
      isLoading,
      getCategoryListAction,
    },
  } = categoryStore;

  const {
    store: { setFilterParams, getProductCardListAction },
  } = productStore;

  const onCategorySelect = async (categoryId: string) => {
    console.log("Category select: " + categoryId);

    setFilterParams({ search: SEARCH });
    setFilterParams({ categoryId: categoryId });
    setFilterParams({ sortProp: SORTPROP });
    setFilterParams({ sortOrder: SORTORDER });

    await getProductCardListAction();

    // await getCategoryByIdAction(); // открыть - передать в параметры категрию, сбросить все остальные фильтры
  };

  const onCategoryShow = async () => {
    await getCategoryListAction();
  };

  if (categoryListError) {
    return <Result title={categoryListError} />;
  }

  return (
    <div>
      <CategoryDrawer
        categoryList={categoryList}
        isLoading={isLoading}
        onCategoryShow={onCategoryShow}
        onCategorySelect={onCategorySelect}
      />
    </div>
  );
});
