export type Category = {
  id: string;
  title: string;
  parentCategoryId: string;
  level: number;
  subcategories: Category[];
};
