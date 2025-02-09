import { makeAutoObservable } from "mobx";
import FilterStore from "../../filter-store/model";
import ProductImageStore from "../../product-image-store/model";
import ProductCardStore from "../../product-card-store/model";
import ProductStore from "../../product-store/model";
import CategoryStore from "../../category-store/model";
import ProfileSellerStore from "../../profile-store/model";

export class RootStore 
{
    filterStore: FilterStore;
    productImageStore: ProductImageStore;
    productCardStore: ProductCardStore;
    productStore: ProductStore;
    categoryStore: CategoryStore;
    profileSellerStore: ProfileSellerStore;

  constructor() {
    this.productStore = new ProductStore();
    this.productCardStore = new ProductCardStore();
    this.categoryStore = new CategoryStore();
    this.filterStore = new FilterStore();
    this.productImageStore = new ProductImageStore();
    this.profileSellerStore = new ProfileSellerStore();

    this.productCardStore.setDependencies({ filterStore: this.filterStore, productImageStore: this.productImageStore });
    this.productStore.setDependencies({ productImageStore: this.productImageStore });

    makeAutoObservable(this); 
  }
}