<template>
  <!-- Sidebar -->
  <div class="w-64 h-96">
    <nav class="mt-6">
      <button
        class="w-full mb-3 hover:bg-primary hover:text-white"
        :class="{ 'bg-primary text-white': activeLink === 'dashboard' }"
        @click="setActiveLink('dashboard')"
      >
        <div class="flex flex-row gap-1 ml-10">
          <svg
            class="mt-2"
            width="35"
            height="35"
            viewBox="0 0 20 20"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              fill-rule="evenodd"
              clip-rule="evenodd"
              d="M2.8 1L2.74967 0.99997C2.52122 0.999752 2.32429 0.999564 2.14983 1.04145C1.60136 1.17312 1.17312 1.60136 1.04145 2.14983C0.999564 2.32429 0.999752 2.52122 0.99997 2.74967L1 2.8V5.2L0.99997 5.25033C0.999752 5.47878 0.999564 5.67572 1.04145 5.85017C1.17312 6.39864 1.60136 6.82688 2.14983 6.95856C2.32429 7.00044 2.52122 7.00025 2.74967 7.00003L2.8 7H5.2L5.25033 7.00003C5.47878 7.00025 5.67572 7.00044 5.85017 6.95856C6.39864 6.82688 6.82688 6.39864 6.95856 5.85017C7.00044 5.67572 7.00025 5.47878 7.00003 5.25033L7 5.2V2.8L7.00003 2.74967C7.00025 2.52122 7.00044 2.32429 6.95856 2.14983C6.82688 1.60136 6.39864 1.17312 5.85017 1.04145C5.67572 0.999564 5.47878 0.999752 5.25033 0.99997L5.2 1H2.8ZM2.38328 2.01382C2.42632 2.00348 2.49222 2 2.8 2H5.2C5.50779 2 5.57369 2.00348 5.61672 2.01382C5.79955 2.05771 5.94229 2.20045 5.98619 2.38328C5.99652 2.42632 6 2.49222 6 2.8V5.2C6 5.50779 5.99652 5.57369 5.98619 5.61672C5.94229 5.79955 5.79955 5.94229 5.61672 5.98619C5.57369 5.99652 5.50779 6 5.2 6H2.8C2.49222 6 2.42632 5.99652 2.38328 5.98619C2.20045 5.94229 2.05771 5.79955 2.01382 5.61672C2.00348 5.57369 2 5.50779 2 5.2V2.8C2 2.49222 2.00348 2.42632 2.01382 2.38328C2.05771 2.20045 2.20045 2.05771 2.38328 2.01382ZM9.8 1L9.74967 0.99997C9.52122 0.999752 9.32429 0.999564 9.14983 1.04145C8.60136 1.17312 8.17312 1.60136 8.04145 2.14983C7.99956 2.32429 7.99975 2.52122 7.99997 2.74967L8 2.8V5.2L7.99997 5.25033C7.99975 5.47878 7.99956 5.67572 8.04145 5.85017C8.17312 6.39864 8.60136 6.82688 9.14983 6.95856C9.32429 7.00044 9.52122 7.00025 9.74967 7.00003L9.8 7H12.2L12.2503 7.00003C12.4788 7.00025 12.6757 7.00044 12.8502 6.95856C13.3986 6.82688 13.8269 6.39864 13.9586 5.85017C14.0004 5.67572 14.0003 5.47878 14 5.25033L14 5.2V2.8L14 2.74967C14.0003 2.52122 14.0004 2.32429 13.9586 2.14983C13.8269 1.60136 13.3986 1.17312 12.8502 1.04145C12.6757 0.999564 12.4788 0.999752 12.2503 0.99997L12.2 1H9.8ZM9.38328 2.01382C9.42632 2.00348 9.49222 2 9.8 2H12.2C12.5078 2 12.5737 2.00348 12.6167 2.01382C12.7995 2.05771 12.9423 2.20045 12.9862 2.38328C12.9965 2.42632 13 2.49222 13 2.8V5.2C13 5.50779 12.9965 5.57369 12.9862 5.61672C12.9423 5.79955 12.7995 5.94229 12.6167 5.98619C12.5737 5.99652 12.5078 6 12.2 6H9.8C9.49222 6 9.42632 5.99652 9.38328 5.98619C9.20045 5.94229 9.05771 5.79955 9.01382 5.61672C9.00348 5.57369 9 5.50779 9 5.2V2.8C9 2.49222 9.00348 2.42632 9.01382 2.38328C9.05771 2.20045 9.20045 2.05771 9.38328 2.01382ZM2.74967 7.99997L2.8 8H5.2L5.25033 7.99997C5.47878 7.99975 5.67572 7.99956 5.85017 8.04145C6.39864 8.17312 6.82688 8.60136 6.95856 9.14983C7.00044 9.32429 7.00025 9.52122 7.00003 9.74967L7 9.8V12.2L7.00003 12.2503C7.00025 12.4788 7.00044 12.6757 6.95856 12.8502C6.82688 13.3986 6.39864 13.8269 5.85017 13.9586C5.67572 14.0004 5.47878 14.0003 5.25033 14L5.2 14H2.8L2.74967 14C2.52122 14.0003 2.32429 14.0004 2.14983 13.9586C1.60136 13.8269 1.17312 13.3986 1.04145 12.8502C0.999564 12.6757 0.999752 12.4788 0.99997 12.2503L1 12.2V9.8L0.99997 9.74967C0.999752 9.52122 0.999564 9.32429 1.04145 9.14983C1.17312 8.60136 1.60136 8.17312 2.14983 8.04145C2.32429 7.99956 2.52122 7.99975 2.74967 7.99997ZM2.8 9C2.49222 9 2.42632 9.00348 2.38328 9.01382C2.20045 9.05771 2.05771 9.20045 2.01382 9.38328C2.00348 9.42632 2 9.49222 2 9.8V12.2C2 12.5078 2.00348 12.5737 2.01382 12.6167C2.05771 12.7995 2.20045 12.9423 2.38328 12.9862C2.42632 12.9965 2.49222 13 2.8 13H5.2C5.50779 13 5.57369 12.9965 5.61672 12.9862C5.79955 12.9423 5.94229 12.7995 5.98619 12.6167C5.99652 12.5737 6 12.5078 6 12.2V9.8C6 9.49222 5.99652 9.42632 5.98619 9.38328C5.94229 9.20045 5.79955 9.05771 5.61672 9.01382C5.57369 9.00348 5.50779 9 5.2 9H2.8ZM9.8 8L9.74967 7.99997C9.52122 7.99975 9.32429 7.99956 9.14983 8.04145C8.60136 8.17312 8.17312 8.60136 8.04145 9.14983C7.99956 9.32429 7.99975 9.52122 7.99997 9.74967L8 9.8V12.2L7.99997 12.2503C7.99975 12.4788 7.99956 12.6757 8.04145 12.8502C8.17312 13.3986 8.60136 13.8269 9.14983 13.9586C9.32429 14.0004 9.52122 14.0003 9.74967 14L9.8 14H12.2L12.2503 14C12.4788 14.0003 12.6757 14.0004 12.8502 13.9586C13.3986 13.8269 13.8269 13.3986 13.9586 12.8502C14.0004 12.6757 14.0003 12.4788 14 12.2503L14 12.2V9.8L14 9.74967C14.0003 9.52122 14.0004 9.32429 13.9586 9.14983C13.8269 8.60136 13.3986 8.17312 12.8502 8.04145C12.6757 7.99956 12.4788 7.99975 12.2503 7.99997L12.2 8H9.8ZM9.38328 9.01382C9.42632 9.00348 9.49222 9 9.8 9H12.2C12.5078 9 12.5737 9.00348 12.6167 9.01382C12.7995 9.05771 12.9423 9.20045 12.9862 9.38328C12.9965 9.42632 13 9.49222 13 9.8V12.2C13 12.5078 12.9965 12.5737 12.9862 12.6167C12.9423 12.7995 12.7995 12.9423 12.6167 12.9862C12.5737 12.9965 12.5078 13 12.2 13H9.8C9.49222 13 9.42632 12.9965 9.38328 12.9862C9.20045 12.9423 9.05771 12.7995 9.01382 12.6167C9.00348 12.5737 9 12.5078 9 12.2V9.8C9 9.49222 9.00348 9.42632 9.01382 9.38328C9.05771 9.20045 9.20045 9.05771 9.38328 9.01382Z"
              fill="currentColor"
            />
          </svg>
          <span
            class="py-2.5 px-4 rounded transition duration-200 font-semibold"
            >Dashboard</span
          >
        </div>
      </button>
      <button
        class="w-full mb-3 hover:bg-primary hover:text-white"
        :class="{ 'bg-primary text-white': activeLink === 'studypal' }"
        @click="setActiveLink('studypal')"
      >
        <div class="flex flex-row gap-2 ml-10">
          <svg
            fill="none"
            height="45"
            viewBox="0 0 45 50"
            width="30"
            xmlns="http://www.w3.org/2000/svg"
          >
            <g clip-rule="evenodd" fill="currentColor" fill-rule="evenodd">
              <path
                d="m13 21c1.6579 0 3-1.3421 3-3s-1.3421-3-3-3-3 1.3421-3 3 1.3421 3 3 3zm0 2c2.7625 0 5-2.2375 5-5s-2.2375-5-5-5-5 2.2375-5 5 2.2375 5 5 5z"
              />
              <path
                d="m5.62151 28.7886c-.48599.4893-.62151.8839-.62151 1.2114v3h18v-3c0-.3275-.1355-.7221-.6215-1.2114-.5043-.5077-1.2895-1.0094-2.3027-1.4522-2.0371-.8903-4.5143-1.3364-6.0758-1.3364s-4.03865.4461-6.07582 1.3364c-1.01321.4428-1.79842.9445-2.30267 1.4522zm8.37849-4.7886c-3.6713 0-11 2.01-11 6v5h22v-5c0-3.99-7.3287-6-11-6z"
              />
              <path
                d="m35 21c1.6579 0 3-1.3421 3-3s-1.3421-3-3-3-3 1.3421-3 3 1.3421 3 3 3zm0 2c2.7625 0 5-2.2375 5-5s-2.2375-5-5-5-5 2.2375-5 5 2.2375 5 5 5z"
              />
              <path
                d="m24 19c1.1054 0 2-.8946 2-2s-.8946-2-2-2-2 .8946-2 2 .8946 2 2 2zm0 2c2.21 0 4-1.79 4-4s-1.79-4-4-4-4 1.79-4 4 1.79 4 4 4z"
              />
              <path
                d="m25.6215 28.7886c-.486.4893-.6215.8839-.6215 1.2114v3h18v-3c0-.3275-.1355-.7221-.6215-1.2114-.5043-.5077-1.2895-1.0094-2.3027-1.4522-2.0371-.8903-4.5143-1.3364-6.0758-1.3364s-4.0387.4461-6.0758 1.3364c-1.0132.4428-1.7984.9445-2.3027 1.4522zm8.3785-4.7886c-3.6713 0-11 2.01-11 6v5h22v-5c0-3.99-7.3287-6-11-6z"
              />
              <path
                d="m24 24c-3.3093 0-5.6283 1.5425-6.2929 2.2071l-1.4143-1.4142c1.0021-1.002 3.8164-2.7929 7.7072-2.7929 3.8907 0 6.705 1.7909 7.7071 2.7929l-1.4143 1.4142c-.6646-.6646-2.9836-2.2071-6.2928-2.2071z"
              />
            </g>
          </svg>
          <span
            class="py-2.5 px-4 rounded transition duration-200 font-semibold"
            >StudyPal</span
          >
        </div>
      </button>
      <button
        class="w-full mb-3 hover:bg-primary hover:text-white"
        :class="{ 'bg-primary text-white': activeLink === 'schedule' }"
        @click="setActiveLink('schedule')"
      >
        <div class="flex flex-row gap-3 ml-10">
          <svg
            class="mt-2"
            width="24"
            height="24"
            stroke-width="1.5"
            viewBox="0 0 20 24"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M15 4V2M15 4V6M15 4H10.5M3 10V19C3 20.1046 3.89543 21 5 21H19C20.1046 21 21 20.1046 21 19V10H3Z"
              stroke="currentColor"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
            <path
              d="M3 10V6C3 4.89543 3.89543 4 5 4H7"
              stroke="currentColor"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
            <path
              d="M7 2V6"
              stroke="currentColor"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
            <path
              d="M21 10V6C21 4.89543 20.1046 4 19 4H18.5"
              stroke="currentColor"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
          <span
            class="mr-11 py-2.5 px-4 rounded transition duration-200 font-semibold"
            >Schedule</span
          >
        </div>
      </button>
      <button
        class="w-full mb-3 hover:bg-primary hover:text-white"
        :class="{ 'bg-primary text-white': activeLink === 'chats' }"
        @click="setActiveLink('chats')"
      >
        <div class="flex flex-row gap-2 ml-10">
          <svg
            class="mt-1"
            viewBox="0 0 256 256"
            width="30"
            height="30"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              fill="currentColor"
              d="m226.24072 189.28271a76.00889 76.00889 0 0 0 -59.4834-112.96671 75.99462 75.99462 0 1 0 -136.998 64.96667l-6.6377 23.2334a9.99929 9.99929 0 0 0 12.36133 12.36328l23.23438-6.63818a75.8485 75.8485 0 0 0 30.49267 9.44926 76.04093 76.04093 0 0 0 108.07275 38.55029l23.23438 6.63867a9.9987 9.9987 0 0 0 12.36133-12.3623zm-166.98926-27.35449a3.99738 3.99738 0 0 0 -1.09863.15381l-24.86719 7.105a2 2 0 0 1 -2.47265-2.47265l7.10449-24.86719a4.00117 4.00117 0 0 0 -.40723-3.14307 67.97064 67.97064 0 1 1 23.78614 23.78613 4.00029 4.00029 0 0 0 -2.04493-.56203zm165.42676 54.75a1.96577 1.96577 0 0 1 -1.96386.50879l-24.86719-7.10449a3.99913 3.99913 0 0 0 -3.14356.4082 68.0381 68.0381 0 0 1 -96.70752-30.54126 75.97949 75.97949 0 0 0 71.50635-95.27636 68.01461 68.01461 0 0 1 48.98828 102.03051 3.99743 3.99743 0 0 0 -.4082 3.14307l7.10449 24.86865a1.96026 1.96026 0 0 1 -.50879 1.96289z"
            />
          </svg>
          <span
            class="mr-11 py-2.5 px-4 rounded transition duration-200 font-semibold"
            >Chats</span
          >
        </div>
      </button>
      <button
        class="w-full mb-3 hover:bg-primary hover:text-white"
        :class="{ 'bg-primary text-white': activeLink === 'settings' }"
        @click="toggleDropdown"
      >
        <div class="flex flex-row gap-3 ml-10">
          <svg
            class="mt-2"
            width="25"
            height="25"
            stroke-width="1.5"
            viewBox="0 0 24 24"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M12 15C13.6569 15 15 13.6569 15 12C15 10.3431 13.6569 9 12 9C10.3431 9 9 10.3431 9 12C9 13.6569 10.3431 15 12 15Z"
              stroke="currentColor"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
            <path
              d="M19.6224 10.3954L18.5247 7.7448L20 6L18 4L16.2647 5.48295L13.5578 4.36974L12.9353 2H10.981L10.3491 4.40113L7.70441 5.51596L6 4L4 6L5.45337 7.78885L4.3725 10.4463L2 11V13L4.40111 13.6555L5.51575 16.2997L4 18L6 20L7.79116 18.5403L10.397 19.6123L11 22H13L13.6045 19.6132L16.2551 18.5155C16.6969 18.8313 18 20 18 20L20 18L18.5159 16.2494L19.6139 13.598L21.9999 12.9772L22 11L19.6224 10.3954Z"
              stroke="currentColor"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
          <span
            class="mr-4 py-2.5 px-4 rounded transition duration-200 font-semibold"
            >Settings</span
          >
          <svg
            v-if="!isDropdownOpen"
            class="mt-2 ml-3"
            width="24"
            height="24"
            stroke-width="1.5"
            viewBox="0 0 24 24"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M6 9L12 15L18 9"
              stroke="currentColor"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
          <svg
            v-if="isDropdownOpen"
            class="mt-2 ml-3"
            width="24"
            height="24"
            stroke-width="1.5"
            viewBox="0 0 24 24"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M6 15L12 9L18 15"
              stroke="currentColor"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </div>
      </button>
      <ul class="ml-16" v-if="isDropdownOpen">
        <li
          class="flex flex-row px-2 py-3 hover:bg-blue-100"
          :class="{ 'bg-blue-100': activeLink === 'profile' }"
        >
          <button class="w-full" @click="setActiveLink('profile')">
            <div class="flex flex-row items-center gap-3">
              <svg
                class="mr-5"
                fill="none"
                height="24"
                viewBox="0 0 24 24"
                width="20"
                xmlns="http://www.w3.org/2000/svg"
              >
                <g
                  stroke="currentColor"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                >
                  <path
                    d="m12 12c2.7614 0 5-2.23858 5-5s-2.2386-5-5-5c-2.76142 0-5 2.23858-5 5s2.23858 5 5 5z"
                  />
                  <path
                    d="m20.5899 22c0-3.87-3.85-7-8.59-7-4.73999 0-8.58999 3.13-8.58999 7"
                  />
                </g>
              </svg>
              <span class="font-semibold">Profile</span>
            </div>
          </button>
        </li>
        <li
          class="flex flex-row px-2 py-3 hover:bg-blue-100"
          :class="{ 'bg-blue-100': activeLink === 'security' }"
        >
          <button class="w-full" @click="setActiveLink('security')">
            <div class="flex flex-row items-center gap-3">
              <svg
                class="mr-4"
                width="22"
                height="22"
                viewBox="0 0 32 32"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="m21 2a9 9 0 0 0 -9 9 8.87 8.87 0 0 0 .39 2.61l-10.39 10.39v6h6l10.39-10.39a9 9 0 0 0 11.61-7.87 8.77 8.77 0 0 0 -1.65-6 9 9 0 0 0 -7.35-3.74zm0 16a7 7 0 0 1 -2-.3l-1.15-.35-.85.85-3.18 3.18-1.41-1.38-1.41 1.41 1.38 1.38-1.59 1.59-1.38-1.38-1.41 1.41 1.38 1.38-2.21 2.21h-3.17v-3.17l9.8-9.83.85-.85-.29-.95a7.14 7.14 0 0 1 3.4-8.44 7 7 0 0 1 10.24 6 6.69 6.69 0 0 1 -1.09 4 7 7 0 0 1 -5.91 3.24z"
                />
                <circle cx="22" cy="10" r="2" />
                <path d="m0 0h32v32h-32z" fill="none" />
              </svg>
              <span class="font-semibold">Security</span>
            </div>
          </button>
        </li>
        <li
          class="flex flex-row px-2 py-3 hover:bg-blue-100"
          :class="{ 'bg-blue-100': activeLink === 'notifications' }"
        >
          <button class="w-full" @click="setActiveLink('notifications')">
            <div class="flex flex-row items-center gap-3">
              <svg
                class="mr-4"
                width="22"
                height="22"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 512 512"
              >
                <path
                  fill="var(--ci-primary-color, currentColor)"
                  d="M450.27,348.569,406.6,267.945V184c0-83.813-68.187-152-152-152s-152,68.187-152,152v83.945L58.928,348.568A24,24,0,0,0,80.031,384h86.935c-.238,2.636-.367,5.3-.367,8a88,88,0,0,0,176,0c0-2.7-.129-5.364-.367-8h86.935a24,24,0,0,0,21.1-35.431ZM310.6,392a56,56,0,1,1-111.419-8H310.018A56.14,56.14,0,0,1,310.6,392ZM93.462,352,134.6,276.055V184a120,120,0,0,1,240,0v92.055L415.736,352Z"
                  class="ci-primary"
                />
              </svg>
              <span class="font-semibold">Notifications</span>
            </div>
          </button>
        </li>
        <li
          class="flex flex-row px-2 py-3 hover:bg-blue-100"
          :class="{ 'bg-blue-100': activeLink === 'notifications' }"
        >
          <button
            class="w-full"
            @click="
              () => {
                userStore.logout();
              }
            "
          >
            <div class="flex flex-row items-center gap-3">
              <svg
                class="mr-4"
                xmlns="http://www.w3.org/2000/svg"
                height="22"
                viewBox="0 0 24 24"
                width="22"
              >
                <path d="M0 0h24v24H0z" fill="none" />
                <path
                  d="M17 7l-1.41 1.41L18.17 11H8v2h10.17l-2.58 2.58L17 17l5-5zM4 5h8V3H4c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h8v-2H4V5z"
                />
              </svg>
              <span class="font-semibold">Logout</span>
            </div>
          </button>
        </li>
      </ul>
    </nav>
  </div>
  <div class="mt-5 border-r-2 border-gray-300 h-screen"></div>
</template>

<script>
import { useUserStore } from "@/store/user";

export default {
  data() {
    return {
      isDropdownOpen: false,
      activeLink: null,
      userStore: useUserStore(),
    };
  },
  methods: {
    toggleDropdown() {
      this.isDropdownOpen = !this.isDropdownOpen;
      this.isDropdownOpen == true
        ? (this.activeLink = "settings")
        : (this.activeLink = null);
    },
    setActiveLink(link) {
      this.activeLink = link;
      if (link == "schedule") {
        this.$router.push("/courses");
      } else {
        this.$router.push(`/${link}`);
      }
    },
  },
};
</script>
