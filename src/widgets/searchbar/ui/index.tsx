import { observer } from "mobx-react-lite";
import {
  CATEGORYID,
  SORTORDER,
  SORTPROP,
} from "../../../entities/stores/filter-store";
import { SearchBar } from "../../../features/search-bar";
import { useStore } from "../../../shared/hooks/store-hook";

export const SearchComponent = observer(() => {
  const {
    filterStore: { FilterParams, setFilterParams },
    productCardStore: { getProductCardListAction },
  } = useStore();

  const handleSearchChange = async (value: string) => {
    setFilterParams({ search: value });
    setFilterParams({ categoryId: CATEGORYID });
    setFilterParams({ sortProp: SORTPROP });
    setFilterParams({ sortOrder: SORTORDER });

    await getProductCardListAction();
  };

  return (
    <>
      <SearchBar
        currentSearchValue={FilterParams.search}
        onValueChange={handleSearchChange}
      />
    </>
  );
});
