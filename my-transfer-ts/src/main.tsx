import React from "react";
import ReactDOM from "react-dom/client";
import {RouterProvider} from "react-router-dom";
import {router} from "./router";
import "./index.css";
import {
    QueryClient,
    QueryClientProvider,
} from "@tanstack/react-query";
import {Provider} from "react-redux";
import {setupStore} from "./store";

const queryClient = new QueryClient();
const store = setupStore();

ReactDOM.createRoot(document.getElementById("root")!).render(
    <React.StrictMode>
        <Provider store={store}>
            <QueryClientProvider client={queryClient}>
                <RouterProvider router={router}/>
            </QueryClientProvider>
        </Provider>
    </React.StrictMode>
);