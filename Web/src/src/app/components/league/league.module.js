'use strict';

import leagueDirective from './league.directive';
import './league.scss';

const leagueModule = angular.module('league-module', []);

leagueModule
  .directive('league', leagueDirective);

export default leagueModule;
