import { createStore } from 'vuex'
import storeStates from './store_states'
import storeGetters from './store_getters'
import storeMutations from './store_mutations'
import storeModules from './store_modules'
import storeActions from './store_actions'

export default createStore({
  state: storeStates,
  getters: storeGetters,
  mutations: storeMutations,
  actions: storeActions,
  modules: storeModules
})
