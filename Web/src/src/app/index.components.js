'use strict';

import footerModule from './components/footer/footer.module';
import headerModule from './components/header/header.module';
import leagueModule from './components/league/league.module';

export default angular.module('index.components', [
	footerModule.name,
	headerModule.name,
	leagueModule.name
]);
