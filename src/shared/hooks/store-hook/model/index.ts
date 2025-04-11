import { useContext } from "react";
import { StoreContext } from "../../../contexts";

export const useStore = () => {
  const store = useContext(StoreContext);
  if (!store) {
    throw new Error("useStore must be used within a StoreProvider");
  }
  return store;
};
