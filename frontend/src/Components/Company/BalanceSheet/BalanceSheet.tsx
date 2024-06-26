import React, { useContext, useEffect, useState } from 'react'
import { CompanyBalanceSheet } from '../../../FinModAPI/CompanyModels';
import { useOutletContext } from 'react-router';
import { getBalanceSheet } from '../../../FinModAPI/FinModApi';
import RatioList from '../../RatioList/RatioList';
import Spinner from '../../Spinner/Spinner';
import { formatLargeMonetaryNumber } from '../../../Helpers/NumberFormatting';

interface Props {}

const config = [
  {
    label: <span className="font-bold">Total Assets</span>,
    render: (company: CompanyBalanceSheet) => formatLargeMonetaryNumber(company.totalAssets),
  },
  {
    label: "Current Assets",
    render: (company: CompanyBalanceSheet) => formatLargeMonetaryNumber(company.totalCurrentAssets),
  },
  {
    label: "Total Cash",
    render: (company: CompanyBalanceSheet) => formatLargeMonetaryNumber(company.cashAndCashEquivalents),
  },
  {
    label: "Property & equipment",
    render: (company: CompanyBalanceSheet) => formatLargeMonetaryNumber(company.propertyPlantEquipmentNet),
  },
  {
    label: "Intangible Assets",
    render: (company: CompanyBalanceSheet) => formatLargeMonetaryNumber(company.intangibleAssets),
  },
  {
    label: "Long Term Debt",
    render: (company: CompanyBalanceSheet) => formatLargeMonetaryNumber(company.longTermDebt),
  },
  {
    label: "Total Debt",
    render: (company: CompanyBalanceSheet) => formatLargeMonetaryNumber(company.otherCurrentLiabilities),
  },
  {
    label: <span className="font-bold">Total Liabilites</span>,
    render: (company: CompanyBalanceSheet) => formatLargeMonetaryNumber(company.totalLiabilities),
  },
  {
    label: "Current Liabilities",
    render: (company: CompanyBalanceSheet) => formatLargeMonetaryNumber(company.totalCurrentLiabilities),
  },
  {
    label: "Long-Term Debt",
    render: (company: CompanyBalanceSheet) => formatLargeMonetaryNumber(company.longTermDebt),
  },
  {
    label: "Long-Term Income Taxes",
    render: (company: CompanyBalanceSheet) => formatLargeMonetaryNumber(company.otherLiabilities),
  },
  {
    label: "Stakeholder's Equity",
    render: (company: CompanyBalanceSheet) => formatLargeMonetaryNumber(company.totalStockholdersEquity),
  },
  {
    label: "Retained Earnings",
    render: (company: CompanyBalanceSheet) => formatLargeMonetaryNumber(company.retainedEarnings),
  },
];

const BalanceSheet = (props: Props) => {
    const ticker = useOutletContext<string>();
    const [balanceSheet, setBalanceSheet] = useState<CompanyBalanceSheet>();

    useEffect(() =>{
        const getData = async () =>{
            const value = await getBalanceSheet(ticker);
            setBalanceSheet(value?.data[0]);
        }
        getData();
    }, []);

  return (
    <>
        { balanceSheet 
        ? <RatioList config={config} data={balanceSheet} />
        : <Spinner />}
    </>
  )
}

export default BalanceSheet