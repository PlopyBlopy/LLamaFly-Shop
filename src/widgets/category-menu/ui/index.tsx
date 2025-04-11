import { observer } from "mobx-react-lite";
import { CategoryDrawer } from "../../../features/category-drawer";
import { Result } from "antd";
import {
  SEARCH,
  SORTORDER,
  SORTPROP,
} from "../../../entities/stores/filter-store";
import { useAppNavigate } from "../../../shared/routing/routes";
import { useStore } from "../../../shared/hooks/store-hook";

export const CategoryComponent = observer(() => {
  const { goToMain } = useAppNavigate();

  const {
    productCardStore: { getProductCardListAction },
    categoryStore: {
      categoryList,
      categoryListError,
      isLoading,
      getCategoryListAction,
    },
    filterStore: { setFilterParams },
  } = useStore();

  const handleCategorySelect = async (categoryId: string) => {
    setFilterParams({ search: SEARCH });
    setFilterParams({ categoryId: categoryId });
    setFilterParams({ sortProp: SORTPROP });
    setFilterParams({ sortOrder: SORTORDER });

    await getProductCardListAction();

    onPageOpen();
  };

  if (categoryListError) {
    return <Result title={categoryListError} />;
  }

  const handleCategoryShow = async () => {
    await getCategoryListAction();
  };

  // useEffect(() => {
  //   const getCategories = async () => {
  //     await getCategoryListAction();
  //   };

  //   if (categoryList.length == 0) {
  //     getCategories();
  //   }
  // }, [categoryList.length]);

  const onPageOpen = () => {
    goToMain();
  };

  return (
    <div>
      <CategoryDrawer
        categoryList={categoryList}
        isLoading={isLoading}
        onCategoryShow={handleCategoryShow}
        onCategorySelect={handleCategorySelect}
      />
    </div>
  );
});
