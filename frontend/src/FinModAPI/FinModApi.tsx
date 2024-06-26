import axios from "axios"
import { CompanyBalanceSheet, CompanyCashFlow, CompanyIncomeStatement, CompanyKeyMetrics, CompanyProfile, CompanySearch } from "./CompanyModels"

export interface SearchResponse {
    data?: CompanySearch[];
    status: number;
    error?: string;
}

export const searchCompanies = async (query: string | undefined) : Promise<SearchResponse> => {
    try{
        const data = await axios.get<CompanySearch[]>(
            `https://financialmodelingprep.com/api/v3/search?query=${query}&limit=10&apikey=${process.env.REACT_APP_API_KEY}`
        );
        return { data: data.data, status: data.status };
    }
    catch (error) {
        if(axios.isAxiosError(error)){
            console.log("Axios Search Error: ", error.message);
            return { status: 100, error: error.message };
        }else{
            console.log("Axios Search unexpected error: ", error);
            return { status: 100, error: "Unexpected error has occured"};
        }
    }
};

export const getCompanyProfile = async (query: string) => {
    try{
        const data = await axios.get<CompanyProfile[]>(
            `https://financialmodelingprep.com/api/v3/profile/${query}?apikey=${process.env.REACT_APP_API_KEY}`
        );
        return data;
    }
    catch(error: any) {
        console.log("Axios Profile Error", error.message);
    }
};

export const getKeyMetrics = async (query: string) => {
    try{
        const data = await axios.get<CompanyKeyMetrics[]>(
            `https://financialmodelingprep.com/api/v3/key-metrics-ttm/${query}?apikey=${process.env.REACT_APP_API_KEY}`
        );
        return data;
    }
    catch(error: any) {
        console.log("Axios Metrics Error", error.message);
    }
};

export const getIncomeStatement = async (query: string) => {
    try{
        const data = await axios.get<CompanyIncomeStatement[]>(
            `https://financialmodelingprep.com/api/v3/income-statement/${query}?limit=40&apikey=${process.env.REACT_APP_API_KEY}`
        );
        return data;
    }
    catch(error: any) {
        console.log("Axios Income Error", error.message);
    }
};

export const getBalanceSheet = async (query: string) => {
    try{
        const data = await axios.get<CompanyBalanceSheet[]>(
            `https://financialmodelingprep.com/api/v3/balance-sheet-statement/${query}?limit=40&apikey=${process.env.REACT_APP_API_KEY}`
        );
        return data;
    }
    catch(error: any) {
        console.log("Axios Balance Error", error.message);
    }
};

export const getCashflowStatement = async (query: string) => {
    try{
        const data = await axios.get<CompanyCashFlow[]>(
            `https://financialmodelingprep.com/api/v3/cash-flow-statement/${query}?period=annual&apikey=${process.env.REACT_APP_API_KEY}`
        );
        return data;
    }
    catch(error: any) {
        console.log("Axios Cashflow Error", error.message);
    }
};