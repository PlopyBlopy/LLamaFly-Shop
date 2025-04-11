import { makeAutoObservable } from "mobx";
import { FilterParams } from "../../../../shared/filter";
import { CATEGORYID, SEARCH, SORTORDER, SORTPROP } from "../config";
// import { injectable } from "tsyringe";

// @injectable()
export default class FilterStore {
  private filterParams: FilterParams = {
    search: SEARCH,
    categoryId: CATEGORYID,
    sortProp: SORTPROP,
    sortOrder: SORTORDER,
  };

  public get FilterParams(): FilterParams {
    return this.filterParams;
  }

  public constructor() {
    makeAutoObservable(this);
  }

  public setFilterParams = (newParams: Partial<FilterParams>) => {
    this.filterParams = { ...this.filterParams, ...newParams };
  };
}
