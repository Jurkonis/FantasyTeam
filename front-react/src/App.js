import React from "react";
import "./App.scss";
import Nav from "./Components/Nav.js";
import Home from "./Components/Home.js";
import Login from "./Components/Login.js";
import Profile from "./Components/Profile/Details.js";
import Register from "./Components/Register.js";
import Schedule from "./Components/Schedule/Schedule.js";
import Tournament from "./Components/Tournament/Tournament.js";
import Tournaments from "./Components/Tournament/TournamentsList.js";
import AddTournament from "./Components/Tournament/AddTournament.js";
import Fantasy from "./Components/FantasyTeam/MyFantasyTeam.js";
import CompareFantasyTeams from "./Components/FantasyTeam/CompareFantasyTeams.js";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import SelectPlayer from "./Components/Team/SelectPlayerList";
import EnableAuth from "./Components/Auth/EnableAuth.js";
import DisableAuth from "./Components/Auth/DisableAuth.js";
import VerifyAuth from "./Components/Auth/VerifyAuth.js";
import Shop from "./Components/Shop/Prizes.js";
import MyIcons from "./Components/Profile/MyIcons.js";

function App() {
  return (
    <Router>
      <div className="App">
        <Nav />
        <Switch>
          <Route path="/" exact component={Home} />
          <Route path="/EnableAuth" component={EnableAuth} />
          <Route path="/DisableAuth" component={DisableAuth} />
          <Route path="/VerifyAuth" component={VerifyAuth} />
          <Route path="/Shop" component={Shop} />
          <Route path="/MyIcons" component={MyIcons} />
          <Route path="/Schedule" component={Schedule} />
          <Route path="/Tournaments" exact component={Tournaments} />
          <Route path="/Tournament/details/:tid" component={Tournament} />
          <Route path="/AddTournament" component={AddTournament} />
          <Route path="/Login" component={Login} />
          <Route path="/Profile" component={Profile} />
          <Route path="/Register" component={Register} />
          <Route path="/MyFantasyTeam" component={Fantasy} />
          <Route path="/User/:id/:username/FantasyTeam" component={CompareFantasyTeams} />
          <Route path="/Team/:tid" exact component={SelectPlayer} />
        </Switch>
      </div>
    </Router>
  );
}

export default App;
