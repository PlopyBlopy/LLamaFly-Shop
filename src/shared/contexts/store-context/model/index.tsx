// store-context/index.ts
import { createContext, useContext, ReactNode, FC } from "react";
import { RootStore } from "../../../../entities/stores/root-store";

const StoreContext = createContext<RootStore | null>(null);

// Явно указываем тип пропсов
export const StoreProvider: FC<{
  store: RootStore;
  children: ReactNode; // Добавляем children
}> = ({ store, children }) => (
  <StoreContext.Provider value={store}>{children}</StoreContext.Provider>
);

export const useStore = () => {
  const store = useContext(StoreContext);
  if (!store) throw new Error("StoreProvider not found!");
  return store;
};
