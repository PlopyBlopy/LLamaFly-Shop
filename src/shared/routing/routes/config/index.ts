export const ROUTES = {
  MAIN: {
    path: "/",
    base: "/",
  },
  PRODUCT: {
    path: (title: string, id: string) => `/product/${title}?id=${id}`,
    base: "/product/:title",
  },
  PROFILE: {
    path: (id: string) => `/profile/?id=${id}`,
    base: "/profile",
  },
  SELLER_PRODUCTS: {
    path: () => `/profile/products`,
    base: "/profile/products",
  },
  ADD_PRODUCT: {
    path: () => "/product/add",
    base: "/product/add",
  },
  SETTINGS: {
    path: () => "/profile/settings",
    base: "/profile/settings",
  },
  REGISTER: {
    path: () => "/profile/register",
    base: "/profile/register",
  },
  REGISTER_SELLER: {
    path: () => "/profile/register/seller",
    base: "/profile/register/seller",
  },
  REGISTER_CUSTOMER: {
    path: () => "/profile/register/customer",
    base: "/profile/register/customer",
  },
  LOGIN: {
    path: () => "/profile/login",
    base: "/profile/login",
  },
} as const;
