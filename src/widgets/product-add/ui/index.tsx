import { observer } from "mobx-react-lite";
import { ProductAddForm } from "../../../features/product-add-form";
import { useStore } from "../../../shared/contexts/store-context";

export const ProductAddComponent = observer(() => {
  const rootStore = useStore();

  return (
    <>
      <ProductAddForm
        onSubmit={(data) => console.log(data)}
        categories={["cat1", "cat2"]}
        sellers={["seller1", "seller2"]}
      />
    </>
  );
});
