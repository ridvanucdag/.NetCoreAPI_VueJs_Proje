 export default {
     getters: {
         count(state) {
             return state.count
         }
     },
     state: () => ({
         count: 0,

     }),
     mutations: {
         increment(state) {
             state.count++
         }
     },

     actions: {
         addOne({
             state,
             commit
         }) {
             commit('increment')
         }
     }
 }