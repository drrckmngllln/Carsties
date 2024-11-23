'use server'

import { auth } from '@/auth'
import React from 'react'

export async function getCurrentUser() {
    try {
        const session = await auth();

        if (!session) return null;

        return session.user;
    } catch (error) {
        return null;
    }
}
