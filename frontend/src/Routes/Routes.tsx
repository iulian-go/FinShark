import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../Pages/HomePage/HomePage";
import SearchPage from "../Pages/SearchPage/SearchPage";
import CompanyPage from "../Pages/CompanyPage/CompanyPage";
import CompanyProfile from "../Components/Company/CompanyProfile/CompanyProfile";
import IncomeStatement from "../Components/Company/IncomeStatement/IncomeStatement";
import DesignGuide from "../Pages/DesignGuide/DesignGuide";
import BalanceSheet from "../Components/Company/BalanceSheet/BalanceSheet";
import CashflowStatement from "../Components/Company/CashflowStatement/CashflowStatement";
import LoginPage from "../Pages/LoginPage/LoginPage";
import RegisterPage from "../Pages/RegisterPage/RegisterPage";
import ProtectedRoute from "./ProtectedRoute";
import ErrorBoundary from "../Components/Error/ErrorBoundary";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        errorElement: <ErrorBoundary/>,
        children: [
            {path: "", element: <HomePage />},
            {path: "login", element: <LoginPage />},
            {path: "register", element: <RegisterPage />},
            {path: "search", element: <ProtectedRoute><SearchPage /></ProtectedRoute>},
            {path: "design-guide", element: <DesignGuide />},
            {path: "company/:ticker", element: <ProtectedRoute><CompanyPage /></ProtectedRoute>,
                children: [
                    {path: "company-profile", element: <CompanyProfile />},
                    {path: "income-statement", element: <IncomeStatement />},
                    {path: "balance-sheet", element: <BalanceSheet />},
                    {path: "cashflow-statement", element: <CashflowStatement />}
                ]
            }
        ]
    }
]);