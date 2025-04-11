import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import mkcert from "vite-plugin-mkcert";
import path from "path";
export default defineConfig({
    plugins: [
        react({
            babel: {
                plugins: [
                    ["@babel/plugin-proposal-decorators", { version: "legacy" }],
                    ["@babel/plugin-transform-class-properties"],
                ],
            },
        }),
        mkcert(),
    ],
    server: {
        host: true,
        strictPort: true,
        port: 5173,
        watch: {
            usePolling: true,
        },
    },
    resolve: {
        alias: {
            "@": path.resolve(__dirname, "./src"),
            features: path.resolve(__dirname, "./src/features"),
            shared: path.resolve(__dirname, "./src/shared"),
        },
    },
});
