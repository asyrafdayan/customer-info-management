const storeGetters = {
    getAPIUrl(state) {
        return state.globals.baseURL
    },
    getStaffDetails(state) {
        return state.globals.staffDetails
    }
}

export default storeGetters
