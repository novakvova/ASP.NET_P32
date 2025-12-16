import React from "react";
import ReactDOM from "react-dom/client";
// import {RouterProvider} from "react-router-dom";
// import {router} from "./router";
import "./index.css";
import {
    QueryClient,
    QueryClientProvider,
} from "@tanstack/react-query";
import {Provider} from "react-redux";
import {setupStore} from "./store";
import {ThemeProvider} from "./admin/context/ThemeContext.tsx";
import {AppWrapper} from "./admin/components/common/PageMeta.tsx";
import App from "./App.tsx";

const queryClient = new QueryClient();
const store = setupStore();

ReactDOM.createRoot(document.getElementById("root")!).render(
    <React.StrictMode>
        <ThemeProvider>
            <AppWrapper>
                <Provider store={store}>
                    <QueryClientProvider client={queryClient}>
                        <App/>
                        {/*<RouterProvider router={router}/>*/}
                    </QueryClientProvider>
                </Provider>
            </AppWrapper>
        </ThemeProvider>
    </React.StrictMode>
);