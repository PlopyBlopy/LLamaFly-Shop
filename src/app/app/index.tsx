import { RouterProvider } from "react-router-dom";
import { Router } from "../routes/index";
import { StoreProvider } from "../../features/store-provider";

//TODO: изменить название папок, компонентов, сервивов на формат product-add-page, user-login-page и т.д.

export const App = () => {
  return (
    <StoreProvider>
      <RouterProvider router={Router} />
    </StoreProvider>
  );
};
