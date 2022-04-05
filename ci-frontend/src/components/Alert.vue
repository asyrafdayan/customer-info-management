<template>
    <div class="alert flex p-4 mb-4 text-sm bg-red-100 text-red-700 rounded-md animate__animated animate__headShake" role="alert">
        <svg class="inline flex-shrink-0 mr-3 w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path></svg>
        <div>
            {{ alertMsg }}
        </div>
    </div>
</template>

<script>
export default {
    props: {
        alertMsg: String
    }
}
</script>
