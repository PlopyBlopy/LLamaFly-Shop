import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { App } from "./app/app/index.tsx";

import { RootStore } from "./entities/stores/root-store";
import { StoreProvider } from "./shared/contexts/store-context/index.ts";

const rootStore = new RootStore();

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <StoreProvider store={rootStore}>
      <App />
    </StoreProvider>
  </StrictMode>
);
