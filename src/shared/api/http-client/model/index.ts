export const API = {
  auth: {
    login: () => "/login",
    logout: () => "/logout",
    registerAdmin: () => "/register/admin",
    registerSeller: () => "/register/seller",
    registerCustomer: () => "/register/customer",
    token: {
      base: "/token",
      refresh: () => `${API.auth.token.base}/refresh`,
      revoke: () => `${API.auth.token.base}/revoke`,
    },
  },
  profiles: {
    profile: () => "/",
    update: () => "/",
    users: {
      base: "/users",
      user: () => `${API.profiles.users.base}`,
      isExist: () => `${API.profiles.users.base}/is-exist`,
      update: () => `${API.profiles.users.base}`,
      delete: () => `${API.profiles.users.base}`,
    },
    admins: {
      base: "/admins",
      admin: () => `${API.profiles.admins.base}`,
      update: () => `${API.profiles.admins.base}`,
    },
    sellers: {
      base: "/sellers",
      seller: () => `${API.profiles.sellers.base}`,
      update: () => `${API.profiles.sellers.base}`,
    },
    customers: {
      base: "/customers",
      customer: () => `${API.profiles.customers.base}`,
      update: () => `${API.profiles.customers.base}`,
    },
  },
  products: {
    add: () => "/",
    productsCards: () => "/",
    sellerProductsCards: (sellerId: string) => `/profile/seller/${sellerId}`,
    detail: (productId: string) => `/detail/${productId}`,
    update: (productId: string) => `/${productId}`,
    delete: (productId: string) => `/${productId}`,
  },
  categories: {
    base: "/categories",
    categories: () => `${API.categories.base}/range`,
  },
  images: {
    products: {
      base: "/products",
      upload: () => `${API.images.products.base}/upload`,
      previews: () => `${API.images.products.base}/preview/range`,
      detail: (productId: string) => `${API.images.products.base}/detail/${productId}`,
      update: () => `${API.images.products.base}`,
      delete: () => `${API.images.products.base}`,
    },
  },
};
