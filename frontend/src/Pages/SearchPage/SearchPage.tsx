import './SearchPage.css'
import React, { useEffect } from 'react'
import { ChangeEvent, SyntheticEvent, useState } from 'react';
import Search from '../../Components/Search/Search';
import { SearchResponse, searchCompanies } from '../../FinModAPI/FinModApi';
import PortfolioList from '../../Components/Portfolio/PortfolioList/PortfolioList';
import CardList from '../../Components/CardList/CardList';
import { PortfolioGet } from '../../Models/Portfolio';
import { portfolioAddAPI, portfolioDeleteAPI, portfolioGetAPI } from '../../Services/PortfolioService';
import { toast } from 'react-toastify';

interface Props {}

const SearchPage = (props: Props) => {
  const [search, setSearch] = useState<string>('');
  const [searchResult, setSearchResult] = useState<SearchResponse>();
  const [portfolioValues, setPortfolioValues] = useState<PortfolioGet[] | null>([]);

  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  }

  const getPortfolio = () => {
    portfolioGetAPI().then((res) => {
      if(res?.data){
        setPortfolioValues(res?.data);
      }
    }).catch(e => toast.warning("Could not get portfolio values!"));
  };

  const onSearchSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();
    const searchResponse = await searchCompanies(search);
    setSearchResult(searchResponse);
  }

  const onPortfolioAdd = (e: any) => {
    e.preventDefault();
    portfolioAddAPI(e.target[0].value).then(res => {
      if(res?.status === 204){
        toast.success("Stock added to portfolio!");
        getPortfolio();
      }
    }).catch(e => toast.warning("Could not create portfolio item!"));
  }

  const onPortfolioDelete = (e: any) => {
    e.preventDefault();
    portfolioDeleteAPI(e.target[0].value).then(res => {
      if(res?.status === 200){
        toast.success("Stock deleted from portfolio!");
        getPortfolio();
      }
    })
  }

  useEffect(() => {
    getPortfolio();
  }, []);

  return (
    <div className="App">
      <Search search={search} onSearchSubmit={onSearchSubmit} handleSearchChange={handleSearchChange} />
      <PortfolioList portfolio={portfolioValues!} onPortfolioDelete={onPortfolioDelete} />
      {searchResult?.status !== 200 && <h2>{searchResult?.error}</h2>}
      <CardList searchResult={searchResult?.data} onPortfolioAdd={onPortfolioAdd} />
    </div>
  )
}

export default SearchPage