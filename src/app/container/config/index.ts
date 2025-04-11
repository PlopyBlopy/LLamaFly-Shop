import "reflect-metadata";
import { container } from "tsyringe";
import {
  ApiExtension,
  ApiInterceptorExtension,
  StoreExtension,
} from "../extensions";
//BUG: НЕ РАБОТАЕТ ЭТОТ ВАШ tsyringe
// Порядок важен!
ApiExtension.addApiServices(container);
StoreExtension.addStoreServices(container);
ApiInterceptorExtension.addApiInterceptorServices(container);

export { container };
