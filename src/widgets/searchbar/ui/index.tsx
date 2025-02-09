import { observer } from "mobx-react-lite";
import { useStore } from "../../../shared/contexts/store-context";
import {
  CATEGORYID,
  SORTORDER,
  SORTPROP,
} from "../../../entities/stores/filter-store";
import { SearchBar } from "../../../features/search-bar";

export const SearchComponent = observer(() => {
  const rootStore = useStore();
  const {
    productCardStore: { getProductCardListAction },
    filterStore: { FilterParams, setFilterParams },
  } = rootStore;

  const handleSearchChange = (value: string) => {
    setFilterParams({ search: value });
    setFilterParams({ categoryId: CATEGORYID });
    setFilterParams({ sortProp: SORTPROP });
    setFilterParams({ sortOrder: SORTORDER });

    getProductCardListAction();
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
