import { observer } from "mobx-react-lite";
import { CategoryDrawer } from "../../../features/category-drawer";
import { Result } from "antd";
import {
  SEARCH,
  SORTORDER,
  SORTPROP,
} from "../../../entities/stores/filter-store";
import { useStore } from "../../../shared/contexts/store-context";
import { useAppNavigate } from "../../../shared/routing/routes";

export const CategoryComponent = observer(() => {
  const { goMain } = useAppNavigate();

  const rootStore = useStore();
  const {
    productCardStore: { getProductCardListAction },
    categoryStore: {
      categoryList,
      categoryListError,
      isLoading,
      getCategoryListAction,
    },
    filterStore: { setFilterParams },
  } = rootStore;

  const onCategorySelect = async (categoryId: string) => {
    console.log("Category select: " + categoryId);

    setFilterParams({ search: SEARCH });
    setFilterParams({ categoryId: categoryId });
    setFilterParams({ sortProp: SORTPROP });
    setFilterParams({ sortOrder: SORTORDER });

    await getProductCardListAction();

    onPageOpen();
  };

  const onCategoryShow = async () => {
    await getCategoryListAction();
  };

  if (categoryListError) {
    return <Result title={categoryListError} />;
  }

  const onPageOpen = () => {
    goMain();
  };

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
