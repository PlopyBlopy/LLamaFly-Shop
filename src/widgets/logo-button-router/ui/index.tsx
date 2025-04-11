import {
  CATEGORYID,
  SEARCH,
  SORTORDER,
  SORTPROP,
} from "../../../entities/stores/filter-store";
import { useStore } from "../../../shared/hooks/store-hook";
import { LogoButton } from "../../../features/logo-button";
import { useAppNavigate } from "../../../shared/routing/routes";

export const LogoRouterComponent = () => {
  const { goToMain } = useAppNavigate();

  const {
    productCardStore: { getProductCardListAction },

    filterStore: { setFilterParams },
  } = useStore();

  const onPageOpen = async () => {
    setFilterParams({ search: SEARCH });
    setFilterParams({ categoryId: CATEGORYID });
    setFilterParams({ sortProp: SORTPROP });
    setFilterParams({ sortOrder: SORTORDER });

    // await getProductCardListAction();
    getProductCardListAction().finally(goToMain); // ✅ Уход от async/await

    goToMain();
  };

  return (
    <>
      <LogoButton onPageOpen={onPageOpen} />
    </>
  );
};
