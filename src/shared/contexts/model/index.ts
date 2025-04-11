import { createContext } from "react";
import { RootStore } from "../../../entities/stores/root-store";

export const StoreContext = createContext<RootStore | null>(null);
